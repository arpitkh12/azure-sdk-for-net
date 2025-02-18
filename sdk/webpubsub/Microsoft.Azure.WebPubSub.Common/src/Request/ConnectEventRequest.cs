﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace Microsoft.Azure.WebPubSub.Common
{
    /// <summary>
    /// Connect event request.
    /// </summary>
    [JsonConverter(typeof(ConnectEventRequestJsonConverter))]
    public sealed class ConnectEventRequest : WebPubSubEventRequest
    {
        internal const string ClaimsProperty = "claims";
        internal const string QueryProperty = "query";
        internal const string SubprotocolsProperty = "subprotocols";
        internal const string ClientCertificatesProperty = "clientCertificates";

        /// <summary>
        /// User Claims.
        /// </summary>
        [JsonPropertyName(ClaimsProperty)]
        public IReadOnlyDictionary<string, string[]> Claims { get; }

        /// <summary>
        /// Request query.
        /// </summary>
        [JsonPropertyName(QueryProperty)]
        public IReadOnlyDictionary<string, string[]> Query { get; }

        /// <summary>
        /// Supported subprotocols.
        /// </summary>
        [JsonPropertyName(SubprotocolsProperty)]
        public IReadOnlyList<string> Subprotocols { get; }

        /// <summary>
        /// Client certificates.
        /// </summary>
        [JsonPropertyName(ClientCertificatesProperty)]
        public IReadOnlyList<WebPubSubClientCertificate> ClientCertificates { get; }

        /// <summary>
        /// Create <see cref="ConnectEventResponse"/>.
        /// </summary>
        /// <param name="userId">Caller userId for current connection.</param>
        /// <param name="subprotocol">Subprotocol applied to current connection.</param>
        /// <param name="roles">User roles applied to current connection.</param>
        /// <param name="groups">Groups applied to current connection.</param>
        /// <returns>A connect response to return service.</returns>
        public ConnectEventResponse CreateResponse(string userId, IEnumerable<string> groups, string subprotocol, IEnumerable<string> roles)
        {
            return new ConnectEventResponse(userId, groups, subprotocol, roles);
        }

        /// <summary>
        /// Create <see cref="EventErrorResponse"/>.
        /// </summary>
        /// <param name="code"><see cref="WebPubSubErrorCode"/>.</param>
        /// <param name="message">Detail error message.</param>
        /// <returns>A error response to return caller and will drop connection.</returns>
        public EventErrorResponse CreateErrorResponse(WebPubSubErrorCode code, string message)
        {
            return new EventErrorResponse(code, message);
        }

        /// <summary>
        /// The connect event request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claims"></param>
        /// <param name="query"></param>
        /// <param name="subprotocols"></param>
        /// <param name="certificates"></param>
        public ConnectEventRequest(
            WebPubSubConnectionContext context,
            IReadOnlyDictionary<string, string[]> claims,
            IReadOnlyDictionary<string, string[]> query,
            IEnumerable<string> subprotocols,
            IEnumerable<WebPubSubClientCertificate> certificates) : base(context)
        {
            if (claims != null)
            {
                Claims = claims;
            }
            if (query != null)
            {
                Query = query;
            }
            Subprotocols = subprotocols?.ToArray();
            ClientCertificates = certificates?.ToArray();
        }
    }
}
