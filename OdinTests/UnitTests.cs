/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Odin <http://www.github.com/Vanaheimr/Odin>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Vanaheimr.Blueprints;
using de.ahzf.Vanaheimr.Blueprints.InMemory;
using de.ahzf.Vanaheimr.Balder;

using NUnit.Framework;

#endregion

namespace de.ahzf.Vanaheimr.Odin.UnitTests
{

    [TestFixture]
    public class UnitTests
    {

        [Test]
        public void testCreateGenericPropertyGraph()
        {
            var _graph = GraphFactory.CreateGenericPropertyGraph(1);
            Assert.NotNull(_graph);
        }

    }

}
