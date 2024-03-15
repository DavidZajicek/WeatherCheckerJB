using System;
using System.Collections.Generic;

public class ApiKeyTracker
{
    private Dictionary<string, List<DateTime>> usageHistory;
    private List<string> _apiKeys;

    public ApiKeyTracker(List<string> apiKeys)
    {
        usageHistory = new Dictionary<string, List<DateTime>>();
        _apiKeys = apiKeys;
    }

    internal bool IsValidApiKey(string apiKey)
    {
        return apiKey.Contains(apiKey);
    }

    public bool CanUseApiKey(string apiKey)
    {
        if (!usageHistory.ContainsKey(apiKey))
        {
            return true;
        }

        var timestamps = usageHistory[apiKey];

        timestamps.RemoveAll(t => DateTime.UtcNow.Subtract(t).TotalHours >= 1);

        return timestamps.Count < 5;
    }

    public void TrackApiKeyUsage(string apiKey)
    {
        if (!usageHistory.ContainsKey(apiKey))
        {
            usageHistory[apiKey] = new List<DateTime>();
        }

        usageHistory[apiKey].Add(DateTime.UtcNow);
    }
}
