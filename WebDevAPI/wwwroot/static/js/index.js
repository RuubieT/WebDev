import Dashboard from "./views/DashboardView.js";
import Contact from "./views/ContactView.js";
import Profile from "./views/ProfileView.js";
import Game from "./views/GameView.js";
import Login from "./views/LoginView.js";
import Register from "./views/RegisterView.js";
import Leaderboard from "./views/LeaderboardView.js";
import { deleteAllButtons } from "./helpers/clearButtons.js";
import Table from "./views/TableView.js";
import Username from "./views/UsernameView.js";
import { removeEventListeners } from "./helpers/verifyForm.js";
import { Auth } from "../models/Auth.js";

const pathToRegex = path => new RegExp("^" + path.replace(/\//g, "\\/").replace(/:\w+/g, "(.+)") + "$");

export var jwtToken = new Auth();

const getParams = match => {
    const values = match.result.slice(1);
    const keys = Array.from(match.route.path.matchAll(/:(\w+)/g)).map(result => result[1]);

    return Object.fromEntries(keys.map((key, i) => {
        return [key, values[i]];
    }));
};

export const navigateTo = url => {
    history.pushState(null, null, url);
    router();
};

const router = async () => {
    const routes = [
        { path: "/", view: Dashboard },
        { path: "/contact", view: Contact },
        { path: "/profile", view: Profile },
        { path: "/game", view: Game},
        { path: "/login", view: Login},
        { path: "/register", view: Register},
        { path: "/leaderboard", view: Leaderboard},
        { path: "/table", view: Table},
        { path: "/username", view: Username}
    ];

    // Test each route for potential match
    const potentialMatches = routes.map(route => {
        return {
            route: route,
            result: location.pathname.match(pathToRegex(route.path))
        };
    });

    let match = potentialMatches.find(potentialMatch => potentialMatch.result !== null);

    if (!match) {
        match = {
            route: routes[0],
            result: [location.pathname]
        };
    }

    const view = new match.route.view(getParams(match));

    document.querySelector("#app").innerHTML = await view.getHtml();
};

window.addEventListener("popstate", router);

document.addEventListener("DOMContentLoaded", () => {
    document.body.addEventListener("click", e => {
        if (e.target.matches("[data-link]")) {
            e.preventDefault();
            removeEventListeners();
            deleteAllButtons();
            navigateTo(e.target.href);
        }
    });

    router();
});
