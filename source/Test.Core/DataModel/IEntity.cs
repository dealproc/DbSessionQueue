namespace Test.Core.DataModel {
	using System;
	public interface IEntity {
		int Id { get; set; }
		DateTime CreatedOnUTC { get; set; }
		DateTime UpdatedOnUTC { get; set; }
	}
}