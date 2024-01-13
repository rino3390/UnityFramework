using GameFramework.Core.Domain;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace GameFramework.Core.Tests
{
	[TestFixture]
	public class RepositoryTest: TestFramework
	{
		[SetUp]
		public override void Setup() { }

		[Test]
		public void Should_Add_Entity_When_NotExist()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player2");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(2, repository.Count, "Repository 儲存數量不正確");
			Assert.AreEqual(entity, repository["Player"], "Repository [Player]  儲存內容不正確");
			Assert.AreEqual(entity2, repository["Player2"], "Repository [Player2] 儲存內容不正確");
		}

		[Test]
		public void Should_Override_Entity_When_Exist()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player");
			var entity3 = Substitute.ForPartsOf<Entity>("Player2");

			repository.Save(entity);
			repository.Save(entity2);
			repository.Save(entity3);

			Assert.AreEqual(2, repository.Count, "Repository 儲存數量不正確");
			Assert.AreEqual(entity2, repository["Player"], "Repository [Player] 讀取的儲存內容不正確");
			Assert.AreEqual(entity3, repository["Player2"], "Repository [Player2] 儲存內容不正確");
		}

		[Test]
		public void Should_Delete_Entity()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player2");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(2, repository.Count, "Repository 儲存數量不正確");

			repository.DeleteById("Player");
			Assert.AreEqual(1, repository.Count, "刪除後的 Repository 儲存數量不正確");
			Assert.AreEqual(entity2, repository.Values.First(), "刪除後的 Repository 儲存內容不正確");
		}

		[Test]
		public void Should_DeleteAll()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player2");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(2, repository.Count, "Repository 儲存數量不正確");

			repository.DeleteAll();
			Assert.AreEqual(0, repository.Count, "刪除後的 Repository 儲存數量不正確");
		}

		[Test]
		public void Should_Not_Add_When_Entity_Is_Null()
		{
			var repository = new Repository<Entity>();

			repository.Save(null);

			Assert.AreEqual(0, repository.Count, "Repository 儲存數量不正確");
		}

		[Test]
		public void Set_Entity_With_Indexer()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player");
			var entity3 = Substitute.ForPartsOf<Entity>("Player2");

			repository["Player"] = entity;
			repository["Player"] = entity2;
			repository["Player"] = entity3;

			Assert.AreEqual(1, repository.Count, "Repository 儲存數量不正確");
			Assert.AreEqual(entity3, repository["Player"], "Repository [Player] 讀取的儲存內容不正確");
		}

		[Test]
		public void Get_Keys_In_Repo()
		{
			var repository = new Repository<Entity>();
			var entity = Substitute.ForPartsOf<Entity>("Player");
			var entity2 = Substitute.ForPartsOf<Entity>("Player2");

			repository.Save(entity);
			repository.Save(entity2);

			Assert.AreEqual(new[] { "Player", "Player2" }, repository.Keys, "Repository Keys 不正確");
		}
	}
}