using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mil.AirForce.Loki.DataAccess.Interfaces;

namespace Mil.AirForce.Loki.DataAccess.Repositories
{
	/// <summary>
	/// Repository and database context class for the application.
	/// </summary>
	public class SqlRepository : DbContext, IRepository
	{
		#region Methods

		/// <summary>
		/// Initializes this class with default properties and underlying class instances.
		/// </summary>
		public void Initialize()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Properties

		#endregion
	}
}
