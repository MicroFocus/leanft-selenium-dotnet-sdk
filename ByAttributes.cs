﻿/*! Copyright 2015 - 2021 Open Text. */
//
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Apache License 2.0 - Apache Software Foundation
// www.apache.org
// Apache License Version 2.0, January 2004 http://www.apache.org/licenses/ TERMS AND CONDITIONS FOR USE, REPRODUCTION ...
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;

namespace LeanFT.Selenium
{
    internal class ByAttributes : ByDictionaryBase
    {
        public ByAttributes(IDictionary<string, object> attributes) : base(attributes)
        {
        }

        protected override string GetScriptName()
        {
            return "GetElementsByAttributes.js";
        }

        protected override string GetDescription()
        {
            return "LFT.Selenium.By.Attributes";
        }
    }
}
