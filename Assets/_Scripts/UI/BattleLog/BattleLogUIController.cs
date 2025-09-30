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
        }


        private void Awake()
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
        }


        private void AddLog(LogEntry logEntry)
        {
            _battleLogUI.AddLog(logEntry.Message, logEntry.Tag);
        }


        private void OnDestroy()
        {
            _battleLogUI.OnToggleLogsButtonClicked -= Toggle;

            _battleLogService.OnAddLog -= AddLog;
            _battleLogService.OnClearLog -= ClearLogs;
        }
    }
}