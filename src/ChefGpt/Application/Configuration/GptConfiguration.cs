﻿// Copyright (c) 2024 Kyle Miller. All rights reserved.
// Licensed under the MIT License. See the LICENSE file in the project root for full license information.

namespace ChefGpt.Application.Configuration
{
    /// <summary>
    ///     Configuration settings for GPT.
    /// </summary>
    public class GptConfiguration
    {
        /// <summary>
        ///     Gets or sets the system prompt for GPT.
        /// </summary>
        public string SystemPrompt { get; set; }

        /// <summary>
        ///     Gets or sets the Recipe Complete Regex.
        ///     When the recipe matches the regex it is considered complete.
        /// </summary>
        public string RecipeCompleteRegex { get; set; }
    }
}