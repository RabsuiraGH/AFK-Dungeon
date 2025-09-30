using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.BattleLog
{
    public class BattleLogUI : TemplateUI
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Button _toggleLogs;
        private TextMeshProUGUI _logTextPrefab;

        private List<TextMeshProUGUI> _allLogs = new();
        private readonly Queue<TextMeshProUGUI> _pool = new();
        private Dictionary<string, List<TextMeshProUGUI>> _activeTaggedLogs = new();
        private List<string> _activeTagsFilter = new();


        public event Action OnToggleLogsButtonClicked;

        public void Init(TextMeshProUGUI logTextPrefab, int poolSize)
        {
            _logTextPrefab = logTextPrefab;

            for (int i = 0; i < poolSize; i++)
            {
                CreateLogInstance();
            }

            _toggleLogs.onClick.AddListener(() => OnToggleLogsButtonClicked?.Invoke());
        }


        public void AddLog(string message, string tag)
        {
            TextMeshProUGUI log = _pool.Count > 0 ? _pool.Dequeue() : CreateLogInstance();

            log.text = message;
            log.gameObject.SetActive(true);

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

            _activeTagsFilter.Remove(tag);
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
            _activeTagsFilter.Clear();
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
            }

            _activeTaggedLogs = new();
            _activeTagsFilter.Clear();
        }


        private void ShowLogsByTag(string tag)
        {
            if (!_activeTaggedLogs.TryGetValue(tag, out var list))
                return;

            _activeTagsFilter.Add(tag);


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
        }
    }
}