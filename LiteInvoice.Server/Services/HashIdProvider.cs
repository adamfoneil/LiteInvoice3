using HashidsNet;
using Microsoft.Extensions.Options;

namespace LiteInvoice.Data.Services;

public class HashIdOptions
{
	public string Salt { get; set; } = default!;
	public int MinLength { get; set; }
}

public class HashIdProvider(IOptions<HashIdOptions> options, Hashids hashIds)
{
	private readonly HashIdOptions Options = options.Value;
	private readonly Hashids HashIds = hashIds;

	public string Encode(int value) => HashIds.Encode(value);

	public int DecodeSingle(string value) => HashIds.DecodeSingle(value);

	public string Encode(IEnumerable<int> values) => HashIds.Encode(values);

	public int[] Decode(string hash) => HashIds.Decode(hash);
}
