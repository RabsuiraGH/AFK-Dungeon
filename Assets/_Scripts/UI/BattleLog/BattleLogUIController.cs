using System.Collections.Generic;
using LA.BattleLog;
using SW.Utilities.LoadAsset;
using TMPro;
using UnityEngine;

namespace LA.UI.BattleLog
{
    public class BattleLogUIController : MonoBehaviour
    {
        [SerializeField] private BattleLogUI _battleLogUI;
        [SerializeField] private int _initialLogPoolSize = 25;
        [SerializeField] private List<string> _existingTags = new();
        [SerializeField] private List<string> _activeTags = new();

        private bool _hasFilter = false;
        private TextMeshProUGUI _logTextPrefab;

        private BattleLogService _battleLogService;


        [VContainer.Inject]
        public void Construct(BattleLogService battleLogService, PathConfig pathConfig)
        {
            _logTextPrefab = LoadAssetUtility.Load<TextMeshProUGUI>(pathConfig.LogTextPrefab);
            _battleLogService = battleLogService;

            _battleLogService.OnAddLog += AddLog;
            _battleLogService.OnClearLog += ClearLogs;

            _battleLogUI.OnToggleLogsButtonClicked += Toggle;
            _battleLogUI.OnFilterSelected += ManageFilter;
        }


        private void ManageFilter(List<string> filterTag)
        {
            _battleLogUI.HideAllLogs();
            foreach (string tag in filterTag)
            {
                if (_existingTags.Contains(tag))
                {
                    _battleLogUI.ShowTaggedLogsAdditionally(tag);
                }
            }

            _activeTags = filterTag;
            _hasFilter = true;
        }


        private void Start()
        {
            _battleLogUI.Init(_logTextPrefab, _initialLogPoolSize);
            _battleLogUI.Hide();
        }


        private void Toggle()
        {
            if (_battleLogUI.IsVisible)
                Hide();
            else
                Show();
        }


        public void Hide() => _battleLogUI.Hide();

        public void Show() => _battleLogUI.Show();


        private void ClearLogs()
        {
            _battleLogUI.ClearAllLogs();
            _existingTags.Clear();
        }


        private void AddLog(LogEntry logEntry)
        {
            bool showLog = !_hasFilter || _activeTags.Contains(logEntry.Tag);
            _battleLogUI.AddLog(logEntry.Message, logEntry.Tag, showLog);

            if (!_existingTags.Contains(logEntry.Tag))
            {
                _existingTags.Add(logEntry.Tag);
                _battleLogUI.AddDropdownOption(logEntry.Tag);
            }
        }


        private void OnDestroy()
        {
            _battleLogUI.OnToggleLogsButtonClicked -= Toggle;

            _battleLogService.OnAddLog -= AddLog;
            _battleLogService.OnClearLog -= ClearLogs;
        }
    }
}