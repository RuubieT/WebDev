import AbstractView from "./AbstractView.js";
import { deleteGameButtons, deletePlayButton, createPokerButtons } from "../helpers/buttons.js";
import { getHand, getPlayers, getTableCards } from "../helpers/gamelogic.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Table");
        
        createPokerButtons();
        getHand();
        getPlayers();
        getTableCards();
        document.getElementById("contact").style.display = 'none';
     
        
    }   

    async getHtml() {
        return `
        <div class="table">
            <div class="card-place" id="tableCardsDiv">
        </div>
        <div class="players">
            <div v-for="(value, index) in players" class="player"
                :class="['player-' + (index + 1), {'playing': player_playing === index}]">
                <div class="bank">
                    <div class="bank-value">{{ value.bank - value.onTable }}</div>
                    <div class="jetons v-10" v-if="(value.bank - value.onTable) / 10 >= 1"></div>
                    <div class="jetons v-5" v-if="(value.bank - value.onTable) / 2 >= 1"></div>
                    <div class="jetons v-1" v-if="(value.bank - value.onTable) >= 1"></div>
                </div>
                <div class="avatar" :style="{backgroundColor: value.color || 'dodgerblue'}"></div>
                <div class="name">{{value.name}}</div>
                <div class="mise">
                    <div class="mise-value">
                        {{ value.onTable }}
                    </div>
                    <div class="jeton-10">
                        <div class="jetons v-10" v-for="(n, i) in ((value.onTable - (value.onTable % 10)) / 10)"
                            :style="{top: (-2 + (i * 5)) + 'px'}" v-if="value.onTable / 10 >= 1"></div>
                    </div>
                    <div class="jeton-5">
                        <div class="jetons v-5"
                            v-for="(n, i) in (((value.onTable % 10) - ((value.onTable % 10) % 2)) / 2)"
                            :style="{top: (-2 + (i * 5)) + 'px'}" v-if="value.onTable % 10 && value.onTable % 10 >= 2">
                        </div>
                    </div>
                    <div class="jeton-1">
                        <div class="jetons v-1" v-if="value.onTable % 10 && value.onTable % 2"></div>
                    </div>
                </div>
            </div>
        </div>
            <div id="cards"></div>
        </div>
        
    

        `;
    }

}

