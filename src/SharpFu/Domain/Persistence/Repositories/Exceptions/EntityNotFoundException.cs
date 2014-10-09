#region

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Exceptions
{
	[Serializable]
	public class EntityNotFoundException : RepositoryException
	{
		public EntityNotFoundException(Type entityType, string searchPredicate)
			: this(entityType, null, searchPredicate)
		{
		}

		public EntityNotFoundException(Type entityType, Type keyType, string searchPredicate)
			: this("Unable to find entity", entityType, keyType, searchPredicate)
		{
		}

		public EntityNotFoundException(string message, Type entityType, string searchPredicate)
			: this(message, entityType, null, searchPredicate)
		{
		}

		public EntityNotFoundException(string message, Type entityType, Type keyType, string searchPredicate)
			: this(message, entityType, keyType, searchPredicate, null)
		{
		}

		public EntityNotFoundException(string message, Type entityType, Type keyType, string searchPredicate, Exception inner)
			: base(message, entityType, keyType, inner)
		{
			SearchPredicate = searchPredicate;
		}

		protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			SearchPredicate = info.GetString("SearchPredicate");
		}

		public string SearchPredicate { get; set; }

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			info.AddValue("SearchPredicate", SearchPredicate);

			base.GetObjectData(info, context);
		}
	}
}