using System;
using System.Collections.Generic;

public class ApiKeyTracker
{
    private Dictionary<string, List<DateTime>> usageHistory;
    private List<string> _apiKeys;
    private Dictionary<string, int> usageLimit { get; set; }

    public ApiKeyTracker(List<string> apiKeys, int defaultUsageLimit)
    {
        usageHistory = new Dictionary<string, List<DateTime>>();
        usageLimit = new Dictionary<string, int>();
        _apiKeys = apiKeys;
        foreach (string apiKey in apiKeys)
        {
            usageLimit.Add(apiKey, defaultUsageLimit);
        }

    }

    public bool IsValidApiKey(string apiKey)
    {
        return _apiKeys.Contains(apiKey);
    }

    public bool CanUseApiKey(string apiKey)
    {
        if (usageLimit[apiKey] == 0)
        {
            return false;
        }
        if (!usageHistory.ContainsKey(apiKey))
        {
            return true;
        }

        var timestamps = usageHistory[apiKey];

        timestamps.RemoveAll(t => DateTime.UtcNow.Subtract(t).TotalHours >= 1);

        return timestamps.Count < usageLimit[apiKey];
    }

    public void TrackApiKeyUsage(string apiKey)
    {
        if (!usageHistory.ContainsKey(apiKey))
        {
            usageHistory[apiKey] = new List<DateTime>();
        }

        usageHistory[apiKey].Add(DateTime.UtcNow);
    }

    public void SetApiKeyUsageLimit(string apiKey, int uses)
    {
        usageLimit[apiKey] = uses;
    }
}
