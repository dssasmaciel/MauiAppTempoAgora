using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Net;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                        if (t != null)
                        {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Pôr do Sol: {t.sunset} \n" +
                                         $"Temperatura Mínima: {t.temp_min} \n" +
                                         $"Temperatura Máxima: {t.temp_max} \n" +
                                         $"Condição: {t.main} \n" +
                                         $"Descrição: {t.description} \n" +
                                         $"Velocidade do Vento: {t.speed} \n" +
                                         $"Visibilidade: {t.visibility} \n";                                       ;

                         lbs_res.Text = dados_previsao;

                        }
                }
                else
                {
                    lbs_res.Text = "Por favor, digite o nome da cidade.";
                }
            }
            catch (InvalidOperationException ex) 
            {
                await DisplayAlert("Erro de Busca", "A cidade informada não foi encontrada. Verifique o nome e tente novamente.", "Ok");
                lbs_res.Text = "Cidade não encontrada.";
            }
            catch (HttpRequestException ex) 
            {
                await DisplayAlert("Erro de Conexão ❌", "Não foi possível conectar ao serviço de clima. Verifique sua conexão com a internet.", "Ok");
                lbs_res.Text = "Falha na conexão.";
            }
            catch (Exception ex) 
            {
                await DisplayAlert("Erro Inesperado ⚠️", $"Ocorreu um erro: {ex.Message}", "Ok");
                lbs_res.Text = "Erro inesperado.";
            }
        }
    }
}