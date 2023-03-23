import AbstractView from "./AbstractView.js";

export default class extends AbstractView {
    constructor(params) {
        super(params);
        this.setTitle("Profile");

        document.getElementById("contact").style.display = 'none';

    }

    async getHtml() {
        return `
        <section class="personalia">

        <img class="person-logo" src="/static/images/Ruben.jpg">
        <h1 class="person-name">Ruben Tharner</h1>

        <h3 class="profile-content__category-title">Personalia</h3>
        <p class="profile-content_info">s1144640@student.windesheim.nl</p>
        <p class="profile-content_info">+31 (0)6 25314295</p>
        <p class="profile-content_info">Tarwekamp36 7908 MR</p>
        <p class="profile-content_info">8 december 2001</p>

        <h3 class="profile-content__category-title">Vaardigheden</h3>
        <p class="profile-content_skills">Oplosingsgericht</p>
        <p class="profile-content_skills">Simplistisch</p>

        <h3 class="profile-content__category-title">Eigenschappen</h3>
        <p class="profile-content_characteristics">Efficiënt</p>
        <p class="profile-content_characteristics">Stilletjes</p>

        
        </section>

    <section class="profile">
        <section class="profile-content">
            <div class="profile-content__category--hidden-mobile">
                <h3 class="profile-content__category-title">Profiel</h3>
                <hr>
            </div>

            <div class="profile-content__element--hidden-mobile">
                <div class="profile-content__element-header">
                </div>
                <p class="profile-content__description">
                    Ik ben Ruben Tharner een 4e jaars HBO-ICT student. Ik ben 21 jaar oud en woon in Hoogeveen.
                <p>
            </div>

            <div class="profile-content__category">
                <h3 class="profile-content__category-title">Opleidingen</h3>
                <hr>
            </div>

            <div class="profile-content__element">
                <div class="profile-content__element-header">
                    <h4 class="profile-content__title">ICT</h4>
                    <p class="profile-content__period">2019 - Nu</p>
                </div>
                <p class="profile-content__institute">Hogeschool Windesheim, Zwolle</p>
            </div>
            <div class="profile-content__element">
                <div class="profile-content__element-header">
                    <h4 class="profile-content__title">HAVO</h4>
                    <p class="profile-content__period">2013 - 2019</p>
                </div>
                <p class="profile-content__institute">RvEC, Hoogeveen</p>
            </div>
        </section>
        `;
    }
}

