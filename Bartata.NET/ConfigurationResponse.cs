using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bartata.NET
{
    [DataContract]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ConfigurationResponse
    {
        [DataMember(Name = "issuer")]
        public string issuer { get; set; }

        [DataMember(Name = "authorization_endpoint")]
        public string authorization_endpoint { get; set; }

        [DataMember(Name = "token_endpoint")]
        public string token_endpoint { get; set; }

        [DataMember(Name = "introspection_endpoint")]
        public string introspection_endpoint { get; set; }

        [DataMember(Name = "userinfo_endpoint")]
        public string userinfo_endpoint { get; set; }

        [DataMember(Name = "end_session_endpoint")]
        public string end_session_endpoint { get; set; }

        [DataMember(Name = "jwks_uri")]
        public string jwks_uri { get; set; }

        [DataMember(Name = "check_session_iframe")]
        public string check_session_iframe { get; set; }

        [DataMember(Name = "grant_types_supported")]
        public List<string> grant_types_supported { get; set; }

        [DataMember(Name = "response_types_supported")]
        public List<string> response_types_supported { get; set; }

        [DataMember(Name = "subject_types_supported")]
        public List<string> subject_types_supported { get; set; }

        [DataMember(Name = "id_token_signing_alg_values_supported")]
        public List<string> id_token_signing_alg_values_supported { get; set; }

        [DataMember(Name = "id_token_encryption_alg_values_supported")]
        public List<string> id_token_encryption_alg_values_supported { get; set; }

        [DataMember(Name = "id_token_encryption_enc_values_supported")]
        public List<string> id_token_encryption_enc_values_supported { get; set; }

        [DataMember(Name = "userinfo_signing_alg_values_supported")]
        public List<string> userinfo_signing_alg_values_supported { get; set; }

        [DataMember(Name = "request_object_signing_alg_values_supported")]
        public List<string> request_object_signing_alg_values_supported { get; set; }

        [DataMember(Name = "response_modes_supported")]
        public List<string> response_modes_supported { get; set; }

        [DataMember(Name = "registration_endpoint")]
        public string registration_endpoint { get; set; }

        [DataMember(Name = "token_endpoint_auth_methods_supported")]
        public List<string> token_endpoint_auth_methods_supported { get; set; }

        [DataMember(Name = "token_endpoint_auth_signing_alg_values_supported")]
        public List<string> token_endpoint_auth_signing_alg_values_supported { get; set; }

        [DataMember(Name = "introspection_endpoint_auth_methods_supported")]
        public List<string> introspection_endpoint_auth_methods_supported { get; set; }

        [DataMember(Name = "introspection_endpoint_auth_signing_alg_values_supported")]
        public List<string> introspection_endpoint_auth_signing_alg_values_supported { get; set; }

        [DataMember(Name = "claims_supported")]
        public List<string> claims_supported { get; set; }

        [DataMember(Name = "claim_types_supported")]
        public List<string> claim_types_supported { get; set; }

        [DataMember(Name = "claims_parameter_supported")]
        public bool claims_parameter_supported { get; set; }

        [DataMember(Name = "scopes_supported")]
        public List<string> scopes_supported { get; set; }

        [DataMember(Name = "request_parameter_supported")]
        public bool request_parameter_supported { get; set; }

        [DataMember(Name = "request_uri_parameter_supported")]
        public bool request_uri_parameter_supported { get; set; }

        [DataMember(Name = "require_request_uri_registration")]
        public bool require_request_uri_registration { get; set; }

        [DataMember(Name = "code_challenge_methods_supported")]
        public List<string> code_challenge_methods_supported { get; set; }

        [DataMember(Name = "tls_client_certificate_bound_access_tokens")]
        public bool tls_client_certificate_bound_access_tokens { get; set; }

        [DataMember(Name = "revocation_endpoint")]
        public string revocation_endpoint { get; set; }

        [DataMember(Name = "revocation_endpoint_auth_methods_supported")]
        public List<string> revocation_endpoint_auth_methods_supported { get; set; }

        [DataMember(Name = "revocation_endpoint_auth_signing_alg_values_supported")]
        public List<string> revocation_endpoint_auth_signing_alg_values_supported { get; set; }

        [DataMember(Name = "backchannel_logout_supported")]
        public bool backchannel_logout_supported { get; set; }

        [DataMember(Name = "backchannel_logout_session_supported")]
        public bool backchannel_logout_session_supported { get; set; }

        [DataMember(Name = "device_authorization_endpoint")]
        public string device_authorization_endpoint { get; set; }

        [DataMember(Name = "backchannel_token_delivery_modes_supported")]
        public List<string> backchannel_token_delivery_modes_supported { get; set; }

        [DataMember(Name = "backchannel_authentication_endpoint")]
        public string backchannel_authentication_endpoint { get; set; }

        internal static ConfigurationResponse FromJson(string json)
        {
            return JsonHelper.Deserialize<ConfigurationResponse>(json);
        }
    }
}
