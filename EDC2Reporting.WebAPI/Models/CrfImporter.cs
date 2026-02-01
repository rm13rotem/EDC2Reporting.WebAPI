
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models.CrfModels;

namespace EDC2Reporting.WebAPI.Models
{
    public class CrfImporter
    {
        private readonly EdcDbContext _db;

        public CrfImporter(EdcDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // 1. Fetch HTML from remote (baseUrl should be like "https://.../Index")
        public async Task<string> GetHtmlFromRemote(string baseUrl, int idOfCrfPageOnRemote)
        {
            if (string.IsNullOrWhiteSpace(baseUrl)) throw new ArgumentException("baseUrl required", nameof(baseUrl));

            var url = $"{baseUrl.TrimEnd('/')}/{idOfCrfPageOnRemote}";
            using var client = new HttpClient();
            var resp = await client.GetAsync(url).ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        // 2. Parse the HTML and create a new CrfPage (Name from first <h1>, Html contains concatenated form-group divs)
        public CrfPage ParseNewCrfPage(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return null!;

            var name = ExtractFirstTagInnerText(html, "h1")?.Trim() ?? $"Imported CRF {Guid.NewGuid():N}";

            var divs = ExtractDivsByClass(html, "form-group");
            var sb = new StringBuilder();
            foreach (var d in divs)
            {
                if (ContainsSubmitControl(d)) continue;

                sb.AppendLine(d);
            }

            var page = new CrfPage
            {
                // Id left to 0 so EF will assign it
                StudyId = 0,
                Name = name,
                Html = sb.ToString(),
                Description = $"Imported from remote on {DateTime.UtcNow:O}",
                CreatedAt = DateTime.UtcNow,
                IsLockedForChanges = false,
                GuidId = Guid.NewGuid().ToString(),
                IsDeleted = false
            };

            return page;
        }

        // 3. Save to DB
        public async Task<CrfPage> SaveAsyncToDb(CrfPage crfPage)
        {
            if (crfPage == null) throw new ArgumentNullException(nameof(crfPage));

            _db.CrfPages.Add(crfPage);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            return crfPage;
        }

        // --- Helper methods ---

        private static string? ExtractFirstTagInnerText(string html, string tagName)
        {
            if (string.IsNullOrEmpty(html)) return null;
            var rx = new Regex($@"\<{tagName}\b[^>]*\>(.*?)\</{tagName}\>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var m = rx.Match(html);
            if (!m.Success) return null;
            var inner = StripHtml(m.Groups[1].Value);
            return inner;
        }

        private static string StripHtml(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        // Robust-ish extraction of <div ... class="form-group"> ... </div> including nested divs
        private static List<string> ExtractDivsByClass(string html, string className)
        {
            var results = new List<string>();
            if (string.IsNullOrEmpty(html)) return results;

            var index = 0;
            while (true)
            {
                // find next <div
                var startTag = html.IndexOf("<div", index, StringComparison.OrdinalIgnoreCase);
                if (startTag == -1) break;

                var tagEnd = html.IndexOf('>', startTag);
                if (tagEnd == -1) break;

                var tagHeader = html.Substring(startTag, tagEnd - startTag + 1);

                // check if this div has the class (class might contain multiple classes)
                var classAttrMatch = Regex.Match(tagHeader, @"\bclass\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                var hasClass = classAttrMatch.Success && classAttrMatch.Groups[1].Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Any(c => string.Equals(c.Trim(), className, StringComparison.OrdinalIgnoreCase));

                if (!hasClass)
                {
                    index = tagEnd + 1;
                    continue;
                }

                // found a starting div with the class; now find its matching closing </div>
                var searchIndex = tagEnd + 1;
                var depth = 1;
                while (depth > 0)
                {
                    var nextOpen = html.IndexOf("<div", searchIndex, StringComparison.OrdinalIgnoreCase);
                    var nextClose = html.IndexOf("</div>", searchIndex, StringComparison.OrdinalIgnoreCase);

                    if (nextClose == -1) // malformed html
                    {
                        searchIndex = html.Length;
                        break;
                    }

                    if (nextOpen != -1 && nextOpen < nextClose)
                    {
                        depth++;
                        searchIndex = nextOpen + 4;
                    }
                    else
                    {
                        depth--;
                        searchIndex = nextClose + 6;
                    }
                }

                var endIndex = Math.Min(searchIndex, html.Length);
                var divHtml = html.Substring(startTag, endIndex - startTag);
                results.Add(divHtml);

                index = endIndex;
            }

            return results;
        }

        // Returns true if the supplied HTML contains an input/button with type="submit"
        private static bool ContainsSubmitControl(string htmlFragment)
        {
            if (string.IsNullOrEmpty(htmlFragment)) return false;

            // matches: <input ... type="submit" ...>  or <button ... type="submit" ...> (with single/double/no quotes)
            var rx = new Regex(@"<\s*(?:input|button)\b[^>]*\btype\s*=\s*[""']?submit[""']?[^>]*>", RegexOptions.IgnoreCase);
            if (rx.IsMatch(htmlFragment)) return true;

            // Also handle self-closing input with attributes in any order; the regex above covers that.
            return false;
        }
    }
}