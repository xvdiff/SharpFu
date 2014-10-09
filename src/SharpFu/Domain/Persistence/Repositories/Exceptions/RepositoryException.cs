#region

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

#endregion

namespace SharpFu.Domain.Persistence.Repositories.Exceptions
{
	[Serializable]
	public class RepositoryException : Exception
	{
		public RepositoryException()
		{
		}

		public RepositoryException(string message)
			: base(message)
		{
		}

		public RepositoryException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public RepositoryException(Type entityType)
			: this(entityType, null)
		{
		}

		public RepositoryException(Type entityType, Type keyType)
			: this(string.Empty, entityType, keyType)
		{
		}

		public RepositoryException(string message, Type entityType)
			: this(message, entityType, null)
		{
		}

		public RepositoryException(string message, Type entityType, Type keyType)
			: this(message, entityType, keyType, null)
		{
		}

		public RepositoryException(string message, Type entityType, Type keyType, Exception inner)
			: this(message, inner)
		{
			EntityType = entityType;
			KeyType = keyType;
		}

		protected RepositoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			EntityType = (Type) info.GetValue("EntityType", typeof (Type));
			KeyType = (Type) info.GetValue("KeyType", typeof (Type));
		}

		public Type EntityType { get; set; }
		public Type KeyType { get; set; }

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			info.AddValue("EntityType", EntityType);
			info.AddValue("KeyType", KeyType);

			base.GetObjectData(info, context);
		}
	}
}