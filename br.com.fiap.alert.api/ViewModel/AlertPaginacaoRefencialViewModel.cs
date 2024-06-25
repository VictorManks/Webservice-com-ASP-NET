namespace br.com.fiap.alert.api.ViewModel;

public class AlertPaginacaoRefencialViewModel
{
        public IEnumerable<AlertViewModel> Alerts { get; set; }
        public int PageSize { get; set; }
        public int Ref { get; set; }
        public int NextRef { get; set; }
        public string PreviousPageUrl => $"/Cliente?referencia={Ref}&tamanho={PageSize}";
        public string NextPageUrl => (Ref < NextRef) ? $"/Cliente?referencia={NextRef}&tamanho={PageSize}" : "";
}
