const API_BASE = "https://localhost:7104";


const JOKE_ENDPOINT = "/api/Jokes";
const CATEGORIES_ENDPOINT = "/api/Categories";

const btnJoke = document.getElementById("btnJoke");
const btnCats = document.getElementById("btnCats");
const err = document.getElementById("err");
const card = document.getElementById("card");
const jid = document.getElementById("jid");
const jdesc = document.getElementById("jdesc");
const catsCard = document.getElementById("catsCard");
const catsList = document.getElementById("catsList");

function setError(message) {
    err.textContent = message ? `Erreur: ${message}` : "";
}

function setLoading(btn, isLoading, textLoading, textNormal) {
    btn.disabled = isLoading;
    btn.textContent = isLoading ? textLoading : textNormal;
}

async function apiGet(endpoint) {
    const res = await fetch(`${API_BASE}${endpoint}`);
    if (!res.ok) throw new Error(`HTTP ${res.status} sur ${endpoint}`);
    return res.json();
}

function show(el, yes) {
    el.style.display = yes ? "block" : "none";
}

btnJoke.onclick = async () => {
    setError("");
    show(card, false);
    setLoading(btnJoke, true, "Chargement...", "Récupérer une blague");

    try {
        const data = await apiGet(JOKE_ENDPOINT);
        console.log("JOKE:", data);
        const text = data.description ?? data.value ?? "";
        jdesc.textContent = text || "Aucun texte trouvé.";

        show(card, true);
    } catch (e) {
        console.error(e);
        setError(e.message || "Erreur");
    } finally {
        setLoading(btnJoke, false, "", "Récupérer une blague");
    }
};

btnCats.onclick = async () => {
    setError("");
    catsList.innerHTML = "";
    show(catsCard, false);
    setLoading(btnCats, true, "Chargement...", "Afficher les catégories");

    try {
        const data = await apiGet(CATEGORIES_ENDPOINT);
        console.log("CATEGORIES:", data);

        if (!Array.isArray(data)) {
            throw new Error("La réponse catégories n'est pas un tableau.");
        }

        data.forEach((c) => {
            const li = document.createElement("li");
            li.textContent = c.description;
            catsList.appendChild(li);
        });

        show(catsCard, true);
    } catch (e) {
        console.error(e);
        setError(e.message || "Erreur");
    } finally {
        setLoading(btnCats, false, "", "Afficher les catégories");
    }
};
