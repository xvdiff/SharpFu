using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using SharpFu.Core.Guarding;
using SharpFu.Domain.Persistence.Repositories;
using SharpFu.Linq.Expressions;

namespace SharpFu.Persistence.MongoDb
{
	public class MongoDbRepository<TEntity, TIdentity> : RepositoryBase<TEntity, TIdentity>
		where TEntity : class
	{
		private readonly Dictionary<Type, IIdGenerator> _keyTypeToBsonGenerator = new Dictionary<Type, IIdGenerator>
		{
			{typeof (string), new StringObjectIdGenerator()},
			{typeof (Guid), new GuidGenerator()},
			{typeof (ObjectId), new ObjectIdGenerator()},
			{typeof (byte[]), new BsonBinaryDataGuidGenerator(GuidRepresentation.Standard)}
		};

		private readonly Dictionary<Type, BsonType> _keyTypeToBsonType = new Dictionary<Type, BsonType>
		{
			{typeof (string), BsonType.String},
			{typeof (Guid), BsonType.ObjectId},
			{typeof (ObjectId), BsonType.ObjectId},
			{typeof (byte[]), BsonType.ObjectId}
		};

		private readonly MongoDatabase _mongoDatabase;

		public MongoDbRepository(MongoDatabase mongoDatabase)
			: this(mongoDatabase, null)
		{

		}

		public MongoDbRepository(MongoDatabase mongoDatabase, Expression<Func<TEntity, TIdentity>> keySelector)
			: base(keySelector)
		{
			Guard.AgainstNullArgument(mongoDatabase, "mongoDatabase");
			_mongoDatabase = mongoDatabase;

			Initialize();
		}

		protected MongoCollection<TEntity> BaseCollection
		{
			get { return _mongoDatabase.GetCollection<TEntity>(EntityType.Name); }
		}

		private void Initialize()
		{
			if (BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
				return;

			var identityName = IdentitySelector.GetMemberInfo().Name;

			BsonClassMap.RegisterClassMap<TEntity>(cm =>
			{
				cm.AutoMap();
				if (cm.IdMemberMap == null)
				{
					cm.SetIdMember(cm.GetMemberMap(identityName));

					if (_keyTypeToBsonType.ContainsKey(typeof(TIdentity)) && (_keyTypeToBsonGenerator.ContainsKey(typeof(TIdentity))))
					{
						cm.IdMemberMap.SetRepresentation(_keyTypeToBsonType[typeof(TIdentity)]);
						cm.IdMemberMap.SetIdGenerator(_keyTypeToBsonGenerator[typeof(TIdentity)]);
					}
				}

				cm.Freeze();
			});
		}


		protected override TEntity GetEntity(TIdentity identity)
		{
			var keyBsonType = ((RepresentationSerializationOptions)BsonClassMap.LookupClassMap(typeof(TEntity))
				.IdMemberMap.SerializationOptions).Representation;

			return IsValidKey(identity)
				? BaseCollection.FindOneById(BsonTypeMapper.MapToBsonValue(identity, keyBsonType))
				: default(TEntity);
		}

		protected override IQueryable<TEntity> ExecuteBaseQuery()
		{
			return BaseCollection.AsQueryable();
		}

		protected override void AddEntity(TEntity entity)
		{
			BaseCollection.Insert(entity);
		}

		protected override void AddOrUpdateEntity(TEntity entity)
		{
			BaseCollection.Save(entity);
		}

		public override void Add(IEnumerable<TEntity> entities)
		{
			BaseCollection.InsertBatch(entities);
		}

		protected override void UpdateEntity(TEntity entity)
		{
			BaseCollection.Save(entity);
		}

		protected override void DeleteEntity(TEntity entity)
		{
			var identity = SafeGetIdentity(entity);
			DeleteEntity(identity);
		}

		protected override void DeleteEntity(TIdentity identity)
		{
			if (!IsValidKey(identity))
				throw new ArgumentException("No valid key for entity.");

			var keyMemberMap = BsonClassMap.LookupClassMap(typeof(TEntity)).IdMemberMap;
			var keyBsonType = ((RepresentationSerializationOptions)keyMemberMap.SerializationOptions).Representation;
			BaseCollection.Remove(Query.EQ(keyMemberMap.ElementName, BsonTypeMapper.MapToBsonValue(identity, keyBsonType)));
		}

		private static bool IsValidKey(TIdentity identity)
		{
			return !String.IsNullOrEmpty(identity.ToString());
		}
	}
}
