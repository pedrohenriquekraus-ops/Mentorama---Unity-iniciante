using TMPro;
using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    [Header("Economia")]
    [SerializeField] int Saldo = 100;
    [SerializeField] int Divida = 100000;
    [SerializeField] int Juros = 10;
    [SerializeField] int Diaria_de_pedreiro = 150;
    [SerializeField] int Diaria_de_Garzon = 300;
    [SerializeField] int Diaria_de_seguranca = 450;
    [SerializeField] int Vender_Droga = 1000;
    [SerializeField] int Aposta_no_tigrinho;
    [SerializeField] int Nivel_de_estudo = 0;
    [SerializeField] int dia_final = 360;
    [SerializeField] int dia_atual = 0;

    enum Escolaridade
    {
        Pedreiro,
        Garzon,
        Seguranca,
    }

    [SerializeField] private Escolaridade nivelAtual = Escolaridade.Pedreiro;

    [Header("UIs")]
    public TextMeshProUGUI DiasAtuaisUI;
    public TextMeshProUGUI EscolaridadeUI;
    public TextMeshProUGUI SaldoUI;
    public TextMeshProUGUI DividaUI;

    public TextMeshProUGUI Eventos;

    void Start()
    {
        AtualizarUI();
    }

    public void AtualizarUI()
    {
        DiasAtuaisUI.text = $"Dia {dia_atual} / {dia_final}";
        EscolaridadeUI.text = nivelAtual.ToString();
        SaldoUI.text = $"{Saldo}";
        DividaUI.text = $"{Divida}";

    }

    void AvancarDia()
    {
        dia_atual++;
        AtualizarUI();
        VerificarFimDeJogo();
    }

    bool VerificarFimDeJogo()
    {
        if (Divida <= 0)
        {
            Eventos.text = "Você pagou a dívida! Você venceu o jogo!";
            return true;
        }

        if (dia_atual >= dia_final)
        {
            Eventos.text = "O prazo acabou e você ainda deve. Você perdeu.";
            return true;
        }

        return false;
    }

    public void Trabalhar()
    {
        int ganho = nivelAtual switch
        {
            Escolaridade.Pedreiro => Diaria_de_pedreiro,
            Escolaridade.Garzon => Diaria_de_Garzon,
            Escolaridade.Seguranca => Diaria_de_seguranca,
            _ => Diaria_de_pedreiro
        };

        Saldo += ganho;
        Eventos.text = $"Você trabalhou como {nivelAtual} e ganhou {ganho}.";
        AvancarDia();
    }

    public void Estudar()
    {
        Nivel_de_estudo++;
        Eventos.text = $"Você estudou. Nível de estudo agora: {Nivel_de_estudo}";

        if (Nivel_de_estudo < 10)
        {
            nivelAtual = Escolaridade.Pedreiro;
        }
        else if (Nivel_de_estudo < 30)
        {
            nivelAtual = Escolaridade.Garzon;
        }
        else
        {
            nivelAtual = Escolaridade.Seguranca;
        }

        AvancarDia();
    }

    public void VenderDroga()
    {
        if (Random.value > 0.5f)
        {
            Saldo += Vender_Droga;
            Eventos.text = $"Você vendeu droga e ganhou {Vender_Droga}.";
        }
        else
        {
            Saldo -= Vender_Droga;
            Eventos.text = $"Você vendeu droga e se lascou, perdeu {Vender_Droga} e ficou preso por 10 dias.";
            dia_atual += 10;
        }

        AvancarDia();
    }

    public void ApostarNoTigrinho()
    {
        Aposta_no_tigrinho = Saldo;

        if (Random.value > 0.5f)
        {
            Saldo += Aposta_no_tigrinho;
            Eventos.text = $"Você ganhou {Aposta_no_tigrinho} no tigrinho!";
        }
        else
        {
            Saldo -= Aposta_no_tigrinho;
            Eventos.text = $"Você perdeu {Aposta_no_tigrinho} no tigrinho!";
        }

        AvancarDia();
    }

    public void PagarDivida()
    {
        Eventos.text = "Você pagou a dívida.";
        Divida -= Saldo;
        Saldo = 0;
        AvancarDia();
    }
}