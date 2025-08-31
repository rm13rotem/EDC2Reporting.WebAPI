using DataServices.SqlServerRepository.Models.CrfModels;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace EDC2Reporting.WebAPI.Services
{
    public class FormRenderingService : IFormRenderingService
    {
        // Very simple hydrator: inject value="..." for inputs whose name matches keys in JSON
        public string Render(CrfPage page, string formDataJson)
        {
            var html = page.Html ?? string.Empty;
            if (string.IsNullOrWhiteSpace(formDataJson)) return html;

            try
            {
                var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(formDataJson!)
                           ?? new Dictionary<string, object>();

                // Handle <input name="X">, <textarea name="X">, <select name="X">
                foreach (var kvp in dict)
                {
                    var key = Regex.Escape(kvp.Key);
                    var val = kvp.Value?.ToString() ?? string.Empty;

                    // input
                    html = Regex.Replace(html,
                        $"(<input[^>]*name=\"{key}\"[^>]*)(>)",
                        m =>
                        {
                            var tag = m.Groups[1].Value;
                            // replace existing value or add one
                            tag = Regex.Replace(tag, "value=\".*?\"", "");
                            return $"{tag} value=\"{System.Net.WebUtility.HtmlEncode(val)}\">";
                        }, RegexOptions.IgnoreCase);

                    // textarea
                    html = Regex.Replace(html,
                        $"(<textarea[^>]*name=\"{key}\"[^>]*>)(.*?)(</textarea>)",
                        m => $"{m.Groups[1].Value}{System.Net.WebUtility.HtmlEncode(val)}{m.Groups[3].Value}",
                        RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    // select (mark matching option as selected)
                    html = Regex.Replace(html,
                        $"(<select[^>]*name=\"{key}\"[^>]*>)(.*?)(</select>)",
                        m =>
                        {
                            var open = m.Groups[1].Value; var body = m.Groups[2].Value; var close = m.Groups[3].Value;
                            body = Regex.Replace(body, "selected\\s*", "", RegexOptions.IgnoreCase);
                            body = Regex.Replace(body, $"(<option[^>]*value=\"{Regex.Escape(val)}\"[^>]*)(>)",
                                mm => mm.Groups[1].Value + " selected>", RegexOptions.IgnoreCase);
                            return open + body + close;
                        },
                        RegexOptions.IgnoreCase | RegexOptions.Singleline);
                }
            }
            catch { /* if JSON is invalid, just return raw HTML */ }

            return html;
        }
    }
}
