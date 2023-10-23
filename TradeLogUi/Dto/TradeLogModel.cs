using MudBlazor;
using TradeLog;

namespace TradeLogUi.Dto;

public class TradeLogModel
{
    public string Id { get; set; }
    public string TraderId { get; set; }
    public string AccountId { get; set; }
    public string Component { get; set; }
    public string ProcessId { get; set; }
    public string OperationId { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string> Data { get; set; }
    public DateTime Date { get; set; }
    public bool ShowDetails { get; set; }

    public TradeLogModel(TradeLogGrpcModel src)
    {
        Id = Guid.NewGuid().ToString();
        TraderId = src.TraderId;
        AccountId = src.AccountId;
        Component = src.Component;
        ProcessId = src.ProcessId;
        OperationId = src.OperationId;
        Message = src.Message;
        Data = src.Data.OrderByDescending(itm => itm.Value.Length).ToDictionary(itm => itm.Key, itm => itm.Value);
        Date = src.Date.ToDateTime();
        ShowDetails = false;
    }
}


public class TradeLogQuery
{
    public string TraderId { get; set; }
    public string AccountId { get; set; }
    public string ProcessId { get; set; }
    public string OperationId { get; set; }
    public string Component { get; set; }
    public DateRange DateRange { get; set; }

    public static TradeLogQuery CreateDefault()
    {
        return new TradeLogQuery
        {
            TraderId = "",
            AccountId = "",
            ProcessId = "",
            OperationId = "",
            Component = "",
            DateRange = new (DateTime.Now.Date, DateTime.Now.AddDays(5).Date)
        };
    }

    public QueryTradeLogGrpcRequest ToGrpc()
    {
        return new QueryTradeLogGrpcRequest
        {
            TraderId = string.IsNullOrEmpty(TraderId) ? null : TraderId,
            AccountId = string.IsNullOrEmpty(AccountId) ? null : AccountId,
            ProcessId = string.IsNullOrEmpty(ProcessId) ? null : ProcessId,
            OperationId = string.IsNullOrEmpty(OperationId) ? null : OperationId,
            Component = string.IsNullOrEmpty(Component) ? null : Component,
            DateFrom = DateRange.Start.Value.ToUnix(),
            DateTo = DateRange.End.Value.ToUnix(),
        };
    }
}

public static class DateUtils
{
    public static uint ToUnix(this DateTime date)
    {
        return (uint)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
    
    public static DateTime ToDateTime(this ulong unixTimeStamp )
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddMicroseconds( unixTimeStamp ).ToLocalTime();
        return dateTime;
    }
}