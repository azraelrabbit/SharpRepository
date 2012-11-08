﻿using System.Linq;
using NUnit.Framework;
using SharpRepository.ODataRepository;
using Should;

namespace SharpRepository.Tests.Integration.Spikes
{
    [TestFixture]
    public class ODataSpikes
    {
        [Test]
        public void NetflixTest()
        {
            var repository = new ODataRepository<Genre>("http://odata.netflix.com/v2/Catalog");
            var results = repository.GetAll();

            results.Count().ShouldEqual(326);
        }

        [Test]
        public void NetflixFindAllTest()
        {
            var repository = new ODataRepository<Title>("http://odata.netflix.com/v2/Catalog");
            var results = repository.FindAll(x => x.ReleaseYear == 1991);

            results.Count().ShouldEqual(326);
        }
    }

    public class Genre
    {
        public string Name { get; set; }
    }

    public class Title
    {
        public string Id { get; set; }
        public string ShortName { get; set; }
        public int ReleaseYear { get; set; }
        public string Rating { get; set; }
    }
}
