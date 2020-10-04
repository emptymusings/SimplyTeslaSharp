using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Requests the API to lock/unlock a car's doors. Extends BasePostResult
    /// </summary>
    internal class DoorLockResults : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public enum LockStates
        {
            Locked,
            Unlocked
        }

        public DoorLockResults(LockStates setToState) :
            base((setToState == LockStates.Locked ? CommandUrls.DoorLock : CommandUrls.DoorUnlock))
        {

        }
    }
}