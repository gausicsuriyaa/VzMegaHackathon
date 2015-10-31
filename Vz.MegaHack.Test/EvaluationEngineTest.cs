using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vz.MegaHack.Engines;
using System.IO;
using Vz.MegaHack.Core;

namespace Vz.MegaHack.Test
{
    [TestFixture]
    public class EvaluationEngineTest
    {
        [Test]
        public void GetLeaderBoard()
        {
            PathManager.Initialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"));
            int intLeaderBoardCount = 0;
            intLeaderBoardCount = EvaluationEngine.GetLeaderBoard("001").LeaderBoard.Count;
            Assert.LessOrEqual(12, intLeaderBoardCount);
        }
    }
}
