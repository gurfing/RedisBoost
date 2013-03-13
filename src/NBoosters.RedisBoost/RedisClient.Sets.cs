using System;
using System.Threading.Tasks;
using NBoosters.RedisBoost.Core;

namespace NBoosters.RedisBoost
{
	public partial class RedisClient
	{
		public Task<long> SAddAsync<T>(string key, params T[] members)
		{
			return SAddAsync(key, Serialize(members));
		}
		public Task<long> SAddAsync(string key, params object[] members)
		{
			return SAddAsync(key, Serialize(members));
		}

		public Task<long> SAddAsync(string key, params byte[][] members)
		{
			if (members.Length == 0)
				throw new ArgumentException("Invalid members count", "members");

			var request = new byte[2 + members.Length][];
			request[0] = RedisConstants.SAdd;
			request[1] = ConvertToByteArray(key);

			for (int i = 0; i < members.Length; i++)
				request[i + 2] = members[i];

			return IntegerResponseCommand(request);
		}

		public Task<long> SRemAsync<T>(string key, params T[] members)
		{
			return SRemAsync(key, Serialize(members));
		}

		public Task<long> SRemAsync(string key, params object[] members)
		{
			return SRemAsync(key, Serialize(members));
		}

		public Task<long> SRemAsync(string key, params byte[][] members)
		{
			if (members.Length == 0)
				throw new ArgumentException("Invalid members count","members");

			var request = new byte[2 + members.Length][];
			request[0] = RedisConstants.SRem;
			request[1] = ConvertToByteArray(key);

			for (int i = 0; i < members.Length; i++)
				request[i + 2] = members[i];

			return IntegerResponseCommand(request);
		}
		public Task<long> SCardAsync(string key)
		{
			return IntegerResponseCommand(RedisConstants.SCard, ConvertToByteArray(key));
		}
		public Task<MultiBulk> SDiffAsync(params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 1][];
			request[0] = RedisConstants.SDiff;
			for (int i = 0; i < keys.Length; i++)
				request[i + 1] = ConvertToByteArray(keys[i]);

			return MultiBulkResponseCommand(request);
		}
		public Task<long> SDiffStoreAsync(string destinationKey, params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 2][];
			request[0] = RedisConstants.SDiffStore;
			request[1] = ConvertToByteArray(destinationKey);
			for (int i = 0; i < keys.Length; i++)
				request[i + 2] = ConvertToByteArray(keys[i]);

			return IntegerResponseCommand(request);
		}
		public Task<MultiBulk> SUnionAsync(params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 1][];
			request[0] = RedisConstants.SUnion;
			for (int i = 0; i < keys.Length; i++)
				request[i + 1] = ConvertToByteArray(keys[i]);

			return MultiBulkResponseCommand(request);
		}
		public Task<long> SUnionStoreAsync(string destinationKey, params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 2][];
			request[0] = RedisConstants.SUnionStore;
			request[1] = ConvertToByteArray(destinationKey);
			for (int i = 0; i < keys.Length; i++)
				request[i + 2] = ConvertToByteArray(keys[i]);

			return IntegerResponseCommand(request);
		}
		public Task<MultiBulk> SInterAsync(params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 1][];
			request[0] = RedisConstants.SInter;
			for (int i = 0; i < keys.Length; i++)
				request[i + 1] = ConvertToByteArray(keys[i]);

			return MultiBulkResponseCommand(request);
		}
		public Task<long> SInterStoreAsync(string destinationKey, params string[] keys)
		{
			if (keys.Length == 0)
				throw new ArgumentException("Invalid number of keys", "keys");

			var request = new byte[keys.Length + 2][];
			request[0] = RedisConstants.SInterStore;
			request[1] = ConvertToByteArray(destinationKey);
			for (int i = 0; i < keys.Length; i++)
				request[i + 2] = ConvertToByteArray(keys[i]);

			return IntegerResponseCommand(request);
		}

		public Task<long> SIsMemberAsync<T>(string key, T value)
		{
			return SIsMemberAsync(key, Serialize(value));
		}

		public Task<long> SIsMemberAsync(string key, byte[] value)
		{
			return IntegerResponseCommand(RedisConstants.SIsMember, ConvertToByteArray(key), value);
		}
		public Task<MultiBulk> SMembersAsync(string key)
		{
			return MultiBulkResponseCommand(RedisConstants.SMembers, ConvertToByteArray(key));
		}

		public Task<long> SMoveAsync<T>(string sourceKey, string destinationKey, T member)
		{
			return SMoveAsync(sourceKey, destinationKey, Serialize(member));
		}

		public Task<long> SMoveAsync(string sourceKey, string destinationKey, byte[] member)
		{
			return IntegerResponseCommand(RedisConstants.SMove, ConvertToByteArray(sourceKey),
				ConvertToByteArray(destinationKey), member);
		}
		public Task<Bulk> SPopAsync(string key)
		{
			return BulkResponseCommand(RedisConstants.SPop, ConvertToByteArray(key));
		}

		public Task<Bulk> SRandMemberAsync(string key)
		{
			return BulkResponseCommand(RedisConstants.SRandMember, ConvertToByteArray(key));
		}
		public Task<MultiBulk> SRandMemberAsync(string key, int count)
		{
			return MultiBulkResponseCommand(RedisConstants.SRandMember, ConvertToByteArray(key),
				ConvertToByteArray(count));
		}
	}
}
