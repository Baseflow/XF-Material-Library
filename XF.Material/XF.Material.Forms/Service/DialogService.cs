//
//  Author:
//    Songurov Fiodor songurov@gmail.com
//
//  Copyright (c) 2019, 
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using System.Collections.Generic;
using System.Threading.Tasks;
using XF.Material.Core.Control.Contract;
using XF.Material.Forms.UI.Dialogs;

namespace XF.Material.Forms.Service
{
    public class DialogService : IDialogService
    {
        public async Task AlertAsync(string message)
        {
            await MaterialDialog.Instance.AlertAsync(message);
        }

        public async Task AlertAsync(string message, string title)
        {
            await MaterialDialog.Instance.AlertAsync(message, title);
        }

        public async Task AlertAsync(string message, string title, string acknowledgementText)
        {
            await MaterialDialog.Instance.AlertAsync(message, title, acknowledgementText);
        }

        public async Task<bool?> ConfirmAsync(string message, string title = null, string confirmingText = "Ok", string dismissiveText = "Cancel")
        {
            return await MaterialDialog.Instance.ConfirmAsync(message, title, confirmingText, dismissiveText);
        }

        public async Task<string> InputAsync(string title = null, string message = null, string inputText = null, string inputPlaceholder = "Enter input", string confirmingText = "Ok", string dismissiveText = "Cancel")
        {
            return await MaterialDialog.Instance.InputAsync(title, message, inputText, inputPlaceholder, confirmingText, dismissiveText);
        }

        public async Task<int> SelectActionAsync(IList<string> actions)
        {
            return await MaterialDialog.Instance.SelectActionAsync(actions);
        }

        public async Task<int> SelectActionAsync(string title, IList<string> actions)
        {
            return await MaterialDialog.Instance.SelectActionAsync(title, actions);
        }

        public async Task<int> SelectChoiceAsync(string title, IList<string> choices)
        {
            return await MaterialDialog.Instance.SelectChoiceAsync(title, choices);
        }

        public async Task<int> SelectChoiceAsync(string title, IList<string> choices, int selectedIndex)
        {
            return await MaterialDialog.Instance.SelectChoiceAsync(title, choices, selectedIndex);
        }

        public async Task<int[]> SelectChoicesAsync(string title, IList<string> choices)
        {
            return await MaterialDialog.Instance.SelectChoicesAsync(title, choices);
        }

        public async Task<int[]> SelectChoicesAsync(string title, IList<string> choices, List<int> selectedIndices)
        {
            return await MaterialDialog.Instance.SelectChoicesAsync(title, choices, selectedIndices);
        }
    }
}
