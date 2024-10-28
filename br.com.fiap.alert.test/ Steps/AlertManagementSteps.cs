using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace br.com.fiap.alert.test.Steps
{
    [Binding]
    public class AlertManagementSteps
    {
        private RestClient _client;
        private RestResponse _response;
        private string _authToken;
        private RestRequest _request;
        private readonly JsonSerializerOptions _jsonOptions;

        public AlertManagementSteps()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        [BeforeScenario]
        public void Setup()
        {
            _client = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri("http://localhost/api/"),
                ThrowOnAnyError = false
            });
        }

        [Given(@"que eu esteja autenticado no sistema")]
        public void DadoQueEuEstejaAutenticadoNoSistema()
        {
            var request = new RestRequest("auth/login", Method.Post);
            var credentials = new
            {
                email = "teste@mail.com",
                name = "Teste",
                passwordHash = "123456"
            };

            request.AddJsonBody(credentials);
            _response = _client.Execute(request);
            
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            _response.Content.Should().NotBeNullOrEmpty();

            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(_response.Content, _jsonOptions);
            tokenResponse.Should().NotBeNull();
            _authToken = tokenResponse.Token;
        }

        [Given(@"possua um token JWT válido")]
        public void DadoPossuaUmTokenJWTValido()
        {
            _authToken.Should().NotBeNullOrEmpty();
        }

        [Given(@"que existam alertas cadastrados no sistema")]
        public void DadoQueExistamAlertasCadastradosNoSistema()
        {
            var createRequest = new RestRequest("alert", Method.Post);
            createRequest.AddHeader("Authorization", $"Bearer {_authToken}");
            
            var newAlert = new
            {
                typeAlert = "TESTE",
                message = "Alerta de teste",
                coords = "-23.550,-46.633",
                author = "Teste Automatizado"
            };

            var jsonBody = JsonSerializer.Serialize(newAlert, _jsonOptions);
            Console.WriteLine($"Corpo da requisição: {jsonBody}");
            
            createRequest.AddStringBody(jsonBody, "application/json");
            var createResponse = _client.Execute(createRequest);
            
            Console.WriteLine($"Status code: {createResponse.StatusCode}");
            Console.WriteLine($"Resposta: {createResponse.Content}");
            
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [When(@"eu enviar uma requisição POST para criar um alerta com os dados:")]
        public void QuandoEuEnviarUmaRequisicaoPOSTParaCriarUmAlertaComOsDados(Table table)
        {
            var alertData = new
            {
                typeAlert = table.Rows[0]["typeAlert"],
                message = table.Rows[0]["message"],
                coords = table.Rows[0]["coords"],
                author = table.Rows[0]["author"]
            };

            _request = new RestRequest("alert", Method.Post);
            _request.AddHeader("Authorization", $"Bearer {_authToken}");
            
            var jsonBody = JsonSerializer.Serialize(alertData, _jsonOptions);
            Console.WriteLine($"Corpo da requisição: {jsonBody}");
            
            _request.AddStringBody(jsonBody, "application/json");
            _response = _client.Execute(_request);
            
            Console.WriteLine($"Status code: {_response.StatusCode}");
            Console.WriteLine($"Resposta: {_response.Content}");
        }

        [When(@"eu solicitar a listagem de alertas com:")]
        public void QuandoEuSolicitarAListagemDeAlertas(Table table)
        {
            var row = table.Rows[0];
            var referencia = int.Parse(row["referencia"].Trim('"'));
            var tamanho = int.Parse(row["tamanho"].Trim('"'));

            _request = new RestRequest("alert", Method.Get);
            _request.AddHeader("Authorization", $"Bearer {_authToken}");
            _request.AddQueryParameter("referencia", referencia.ToString());
            _request.AddQueryParameter("tamanho", tamanho.ToString());
            
            _response = _client.Execute(_request);
            Console.WriteLine($"Status code: {_response.StatusCode}");
            Console.WriteLine($"Resposta: {_response.Content}");
        }

        [When(@"eu tentar listar os alertas")]
        public void QuandoEuTentarListarOsAlertas()
        {
            _request = new RestRequest("alert", Method.Get);
            _response = _client.Execute(_request);
            Console.WriteLine($"Status code: {_response.StatusCode}");
            Console.WriteLine($"Resposta: {_response.Content}");
        }

        [Then(@"o sistema deve retornar o status code (.*)")]
        public void EntaoOSistemaDeveRetornarOStatusCode(int statusCode)
        {
            ((int)_response.StatusCode).Should().Be(statusCode);
        }

        [Then(@"retornar o ID do novo alerta criado")]
        public void EntaoRetornarOIDDoNovoAlertaCriado()
        {
            var locationHeader = _response.Headers.FirstOrDefault(h => h.Name == "Location");
            locationHeader.Should().NotBeNull();
            locationHeader.Value.Should().NotBeNull();
            Console.WriteLine($"Location Header: {locationHeader.Value}");
        }

        [Then(@"retornar uma lista com no máximo (.*) alertas")]
        public void EntaoRetornarUmaListaComNoMaximoAlertas(int maxAlertas)
        {
            _response.Content.Should().NotBeNullOrEmpty();
            var responseData = JsonSerializer.Deserialize<AlertPaginationResponse>(_response.Content, _jsonOptions);
            
            responseData.Should().NotBeNull();
            responseData.Alerts.Should().NotBeNull();
            responseData.Alerts.Count().Should().BeLessOrEqualTo(maxAlertas);
        }

        private class TokenResponse
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }

        private class AlertPaginationResponse
        {
            [JsonPropertyName("alerts")]
            public IEnumerable<AlertViewModel> Alerts { get; set; }

            [JsonPropertyName("pageSize")]
            public int PageSize { get; set; }

            [JsonPropertyName("ref")]
            public int Ref { get; set; }

            [JsonPropertyName("nextRef")]
            public int NextRef { get; set; }
        }

        private class AlertViewModel
        {
            [JsonPropertyName("alertId")]
            public int AlertId { get; set; }

            [JsonPropertyName("typeAlert")]
            public string TypeAlert { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("coords")]
            public string Coords { get; set; }

            [JsonPropertyName("author")]
            public string Author { get; set; }
        }
    }
}