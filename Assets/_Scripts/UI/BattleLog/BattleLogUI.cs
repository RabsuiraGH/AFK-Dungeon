using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.BattleLog
{
    public class BattleLogUI : TemplateUI
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _toggleLogs;

        [SerializeField] private TMP_Dropdown _filterDropdown;
        private TextMeshProUGUI _logTextPrefab;

        private List<TextMeshProUGUI> _allLogs = new();
        private readonly Queue<TextMeshProUGUI> _pool = new();
        private Dictionary<string, List<TextMeshProUGUI>> _activeTaggedLogs = new();

        public event Action OnToggleLogsButtonClicked;
        public event Action<List<string>> OnFilterSelected;


        public void Init(TextMeshProUGUI logTextPrefab, int poolSize)
        {
            _logTextPrefab = logTextPrefab;

            for (int i = 0; i < poolSize; i++)
            {
                CreateLogInstance();
            }

            _toggleLogs.onClick.AddListener(() => OnToggleLogsButtonClicked?.Invoke());

            // OMG, this is absolutely horrid event! Who the hell decided to use bitmasks?
            _filterDropdown.onValueChanged.AddListener(GetSelectedFilters);
        }


        public void AddLog(string message, string tag, bool isActive = true)
        {
            TextMeshProUGUI log = _pool.Count > 0 ? _pool.Dequeue() : CreateLogInstance();

            log.text = message;
            log.gameObject.SetActive(isActive);

            _allLogs.Add(log);

            if (_activeTaggedLogs.TryGetValue(tag, out var list))
            {
                list.Add(log);
            }
            else
            {
                _activeTaggedLogs[tag] = new List<TextMeshProUGUI> { log };
            }
        }


        public void HideLogsByTag(string tag)
        {
            if (!_activeTaggedLogs.TryGetValue(tag, out var list))
            {
                return;
            }

            foreach (TextMeshProUGUI log in list)
            {
                log.gameObject.SetActive(false);
            }
        }


        public void ShowAllLogs()
        {
            foreach (List<TextMeshProUGUI> list in _activeTaggedLogs.Values)
            {
                foreach (TextMeshProUGUI log in list)
                {
                    log.gameObject.SetActive(true);
                }
            }
        }


        public void ShowTaggedLogs(string tag)
        {
            HideAllLogs();
            ShowLogsByTag(tag);
        }


        public void ShowTaggedLogsAdditionally(string tag)
        {
            ShowLogsByTag(tag);
        }


        public void HideAllLogs()
        {
            foreach (var log in _allLogs)
            {
                log.gameObject.SetActive(false);
            }
        }


        public void ClearAllLogs()
        {
            foreach (var log in _allLogs)
            {
                _pool.Enqueue(log);
                log.gameObject.SetActive(false);
            }

            _activeTaggedLogs = new();
        }


        public void SetDropdownOptions(List<string> options) =>
            _filterDropdown.options = options.Select(x => new TMP_Dropdown.OptionData(x)).ToList();


        public void AddDropdownOption(string option) =>
            _filterDropdown.options.Add(new TMP_Dropdown.OptionData(option));


        private void GetSelectedFilters(int mask)
        {
            List<string> result = new();
            for (int j = 0; j < _filterDropdown.options.Count; j++)
            {
                if ((mask & (1 << j)) != 0)
                {
                    result.Add(_filterDropdown.options.ElementAt(j).text);
                }
            }

            OnFilterSelected?.Invoke(result);
        }


        private void ShowLogsByTag(string tag)
        {
            if (!_activeTaggedLogs.TryGetValue(tag, out var list))
                return;

            foreach (TextMeshProUGUI log in list)
            {
                log.gameObject.SetActive(true);
            }
        }


        private TextMeshProUGUI CreateLogInstance()
        {
            TextMeshProUGUI log = Instantiate(_logTextPrefab, _content);
            log.gameObject.SetActive(false);
            return log;
        }


        private void OnDestroy()
        {
            _toggleLogs.onClick.RemoveAllListeners();
            _filterDropdown.onValueChanged.RemoveAllListeners();
        }
    }
}