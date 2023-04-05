﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

namespace Microsoft.Azure.WebJobs.Extension.Dapr
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Newtonsoft.Json.Linq;

    class DaprSecretConverter :
        IAsyncConverter<DaprSecretAttribute, byte[]>,
        IAsyncConverter<DaprSecretAttribute, string?>,
        IAsyncConverter<DaprSecretAttribute, IDictionary<string, string>>,
        IAsyncConverter<DaprSecretAttribute, JsonElement>,
        IAsyncConverter<DaprSecretAttribute, JObject>,
        IAsyncConverter<DaprSecretAttribute, object?>
    {
        readonly DaprServiceClient daprClient;

        public DaprSecretConverter(DaprServiceClient daprClient)
        {
            this.daprClient = daprClient;
        }

        async Task<byte[]> IAsyncConverter<DaprSecretAttribute, byte[]>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            JsonDocument result = await this.GetSecretsAsync(input, cancellationToken);
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));
        }

        async Task<string?> IAsyncConverter<DaprSecretAttribute, string?>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            JsonDocument result = await this.GetSecretsAsync(input, cancellationToken);
            return JsonSerializer.Serialize(result);
        }

        async Task<IDictionary<string, string>> IAsyncConverter<DaprSecretAttribute, IDictionary<string, string>>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            JsonDocument result = await this.GetSecretsAsync(input, cancellationToken);
            var obj = JsonSerializer.Deserialize<Dictionary<string, string>>(result);
            return obj ?? new Dictionary<string, string>();
        }

        async Task<JsonElement> IAsyncConverter<DaprSecretAttribute, JsonElement>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            return (await this.GetSecretsAsync(input, cancellationToken)).RootElement;
        }

        async Task<JObject> IAsyncConverter<DaprSecretAttribute, JObject>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            JsonDocument result = await this.GetSecretsAsync(input, cancellationToken);
            return JObject.Parse(JsonSerializer.Serialize(result));
        }

        async Task<object?> IAsyncConverter<DaprSecretAttribute, object?>.ConvertAsync(
            DaprSecretAttribute input,
            CancellationToken cancellationToken)
        {
            JsonDocument result = await this.GetSecretsAsync(input, cancellationToken);
            return JsonSerializer.Deserialize<object>(result);
        }

        Task<JsonDocument> GetSecretsAsync(DaprSecretAttribute input, CancellationToken cancellationToken)
        {
            return this.daprClient.GetSecretAsync(
                input.DaprAddress,
                input.SecretStoreName,
                input.Key,
                input.Metadata,
                cancellationToken);
        }
    }
}