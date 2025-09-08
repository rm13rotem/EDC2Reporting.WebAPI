using Microsoft.AspNetCore.Http;

namespace SessionLayer
{

    public class SessionWrapper : ISessionWrapper
    {
        private readonly ISession _session;

        private const string SelectedPatientKey = "SelectedPatientId";
        private const string SelectedVisitGroupKey = "SelectedVisitGroupId";
        private const string SelectedVisitIndexKey = "SelectedVisitIndex";
        private const string SelectedVisitIdKey = "SelectedVisitId";
        private const string SelectedCrfIdKey = "SelectedCrfId";

        public SessionWrapper(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public int SelectedPatientId
        {
            get => _session.GetInt32(SelectedPatientKey) ?? 0;
            set => _session.SetInt32(SelectedPatientKey, value);
        }

        public int SelectedVisitGroupId
        {
            get => _session.GetInt32(SelectedVisitGroupKey) ?? 0;
            set => _session.SetInt32(SelectedVisitGroupKey, value);
        }

        public int SelectedVisitIndex
        {
            get => _session.GetInt32(SelectedVisitIndexKey) ?? 0;
            set => _session.SetInt32(SelectedVisitIndexKey, value);
        }

        public int SelectedVisitId
        {
            get => _session.GetInt32(SelectedVisitIdKey) ?? 0;
            set => _session.SetInt32(SelectedVisitIdKey, value);
        }

        public int SelectedCrfId
        {
            get => _session.GetInt32(SelectedCrfIdKey) ?? 0;
            set => _session.SetInt32(SelectedCrfIdKey, value);
        }
    }
}
