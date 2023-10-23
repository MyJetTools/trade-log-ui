using SimpleTrading.SettingsReader;

[YamlAttributesOnly]
public class SettingsModel
{
    [YamlProperty("TradeLogGrpc")]
    public string TradeLogGrpc { get; set; }
}