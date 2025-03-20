const apiBaseUrl = "http://localhost:5043/api";

// Função de login
async function login() {
    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    if (!email || !senha) {
        alert("Por favor, preencha todos os campos.");
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/auth/login`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, senha })
        });

        if (!response.ok) {
            throw new Error("Falha no login. Verifique suas credenciais.");
        }

        const data = await response.json();
        localStorage.setItem("token", data.token);
        alert("Login bem-sucedido!");
        window.location.href = "menu.html";
    } catch (error) {
        alert(error.message);
    }
}

// Função para verificar autenticação e redirecionar se necessário
function verificarLogin() {
    const token = localStorage.getItem("token");
    if (!token) {
        alert("Você precisa estar logado para acessar esta página.");
        window.location.href = "index.html";
    }
}

// Função para carregar dúvidas
async function carregarDuvidas() {
    try {
        const response = await fetch(`${apiBaseUrl}/duvidas`, {
            headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` }
        });

        if (!response.ok) throw new Error("Erro ao carregar dúvidas.");

        const duvidas = await response.json();
        document.getElementById("listaDuvidas").innerHTML = duvidas
            .map(d => `<p>${d.pergunta}: <b>${d.resposta}</b></p>`)
            .join("");
    } catch (error) {
        alert(error.message);
    }
}

// Função para adicionar dúvida
async function adicionarDuvida() {
    const pergunta = document.getElementById("novaDuvida").value;
    const resposta = document.getElementById("novaResposta").value;

    if (!pergunta || !resposta) {
        alert("Por favor, preencha os campos.");
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/duvidas`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ pergunta, resposta })
        });

        if (!response.ok) throw new Error("Erro ao adicionar dúvida.");

        alert("Dúvida adicionada!");
        carregarDuvidas();
    } catch (error) {
        alert(error.message);
    }
}

// Função para definir alarme
async function definirAlarme() {
    const horario = document.getElementById("alarme").value;

    if (!horario) {
        alert("Por favor, selecione um horário.");
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/alarmes`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            },
            body: JSON.stringify({ horario })
        });

        if (!response.ok) throw new Error("Erro ao definir alarme.");

        document.getElementById("alarmeAtivo").innerText = `Alarme para ${horario}`;
        alert("Alarme definido com sucesso!");
    } catch (error) {
        alert(error.message);
    }
}

// Função para logout
function logout() {
    localStorage.removeItem("token");
    alert("Logout realizado com sucesso!");
    window.location.href = "index.html";
}

// Função para carregar o quiz dinamicamente
let perguntaAtual = {};
let respostaCorreta = "";

async function carregarQuiz() {
    const usuario = JSON.parse(localStorage.getItem("usuario"));

    if (!usuario) {
        alert("Você precisa estar logado para acessar esta página!");
        window.location.href = "index.html";
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/quiz`, {
            headers: { "Authorization": `Bearer ${localStorage.getItem("token")}` }
        });

        if (!response.ok) throw new Error("Erro ao carregar quiz.");

        const quizzes = await response.json();
        perguntaAtual = quizzes[Math.floor(Math.random() * quizzes.length)];

        document.getElementById("pergunta").innerText = perguntaAtual.pergunta;
        respostaCorreta = perguntaAtual.resposta;

        const opcoesElement = document.getElementById("opcoes");
        opcoesElement.innerHTML = perguntaAtual.opcoes.map(opcao => `
            <li>
                <input type="radio" name="quiz" value="${opcao}"> ${opcao}
            </li>
        `).join("");
    } catch (error) {
        document.getElementById("pergunta").innerText = "Erro ao carregar quiz!";
        alert(error.message);
    }
}

// Função para responder o quiz
function responderQuiz() {
    const selecionado = document.querySelector('input[name="quiz"]:checked');

    if (!selecionado) {
        alert("Escolha uma opção antes de responder!");
        return;
    }

    const respostaUsuario = selecionado.value;
    const feedbackElement = document.getElementById("feedback");

    if (respostaUsuario === respostaCorreta) {
        feedbackElement.innerText = "✅ Parabéns! Resposta correta!";
        feedbackElement.style.color = "green";
    } else {
        feedbackElement.innerText = "❌ Resposta errada! Tente novamente.";
        feedbackElement.style.color = "red";
    }
}
