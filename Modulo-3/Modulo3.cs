using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    [SerializeField] int Saldo = 100000;
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
    private bool aguardandoEscolha = false;

    void Start()
    {
        Dia_a_dia();
    }

    void Update()
    {
        if (!aguardandoEscolha) return; // só escuta tecla quando o menu do dia está ativo

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Trabalhar();
            AvancarDia();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Estudar();
            AvancarDia();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            VenderDroga();
            AvancarDia();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ApostarNoTigrinho();
            AvancarDia();
        }
    }

    public void Dia_a_dia()
    {
        Debug.Log($"Voce esta no dia {dia_atual}, falta {dia_final - dia_atual} para o prazo. Divida atual: {Divida}");
        Debug.Log("Oque voce gosta de fazer hoje?");
        Debug.Log("1 - Trabalhar");
        Debug.Log("2 - Estudar");
        Debug.Log("3 - Vender droga (da uma grana mas cuidado)");
        Debug.Log("4 - Apostar no tigrinho");

        aguardandoEscolha = true;
    }

    void AvancarDia()
    {
        aguardandoEscolha = false;
        dia_atual++;

        if (VerificarFimDeJogo()) return;

        Dia_a_dia(); // mostra o menu do próximo dia, reinicia o ciclo
    }

    bool VerificarFimDeJogo()
    {
        if (Divida <= 0)
        {
            Debug.Log("Você pagou a dívida! Você venceu o jogo!");
            return true;
        }

        if (dia_atual >= dia_final)
        {
            Debug.Log("O prazo acabou e você ainda deve. Você perdeu.");
            return true;
        }

        return false;
    }

    void Trabalhar()
    {
        int ganho = nivelAtual switch
        {
            Escolaridade.Pedreiro => Diaria_de_pedreiro,
            Escolaridade.Garzon => Diaria_de_Garzon,
            Escolaridade.Seguranca => Diaria_de_seguranca,
            _ => Diaria_de_pedreiro
        };

        Saldo += ganho;
        Debug.Log($"Você trabalhou como {nivelAtual} e ganhou {ganho}.");
    }

    void Estudar()
    {
        Nivel_de_estudo++;
        Debug.Log($"Você estudou. Nível de estudo agora: {Nivel_de_estudo}");
        if (Nivel_de_estudo < 10)
        {
            nivelAtual = Escolaridade.Pedreiro;
        }
        else
        if (Nivel_de_estudo > 10 && Nivel_de_estudo < 30)
        {
            nivelAtual = Escolaridade.Garzon;
        }
        else
        if (Nivel_de_estudo > 10 && Nivel_de_estudo < 30)
        {
            nivelAtual = Escolaridade.Seguranca;
        }
    }

    void VenderDroga()
    {

        if (Random.value > 0.5f)
        {
            Saldo += Vender_Droga;
            Debug.Log($"Você vendeu droga e ganhou {Vender_Droga}.");
        }
        else
        {
            Saldo -= Aposta_no_tigrinho;
            Debug.Log($"Você vendeu droga e se lascou perdeu {Vender_Droga}, e fico preso 30  10 dias.");
            dia_atual += 10;
        }

    }

    void ApostarNoTigrinho()
    {
        Aposta_no_tigrinho = Saldo;


        if (Random.value > 0.5f)
        {
            Saldo += Aposta_no_tigrinho;
            Debug.Log($"Você ganhou {Aposta_no_tigrinho} no tigrinho!");
        }
        else
        {
            Saldo -= Aposta_no_tigrinho;
            Debug.Log($"Você perdeu {Aposta_no_tigrinho} no tigrinho!");
        }
    }
}