// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AutoProjectRenamer.Contracts
{
    public interface IReporter
    {
        void WriteLine(string message);

        void WriteLine();

        void Write(string message);
    }
}
