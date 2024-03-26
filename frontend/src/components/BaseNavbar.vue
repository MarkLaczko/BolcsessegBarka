<template>
  <nav class="navbar navbar-expand-lg bg-info" data-bs-theme="dark">
  <div class="container-fluid">
    <RouterLink class="navbar-brand"  :to="{name: 'home'}"><img class="img-fluid" src="../assets/images/logo.png" alt="BölcsességBárka" style="max-width: 50px;"></RouterLink>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <RouterLink class="nav-link"  :to="{name: 'home'}">Főoldal</RouterLink>
        </li>
        <li class="nav-item">
          <RouterLink class="nav-link"  :to="{}">Kurzusaim</RouterLink>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            Adminisztráció
          </a>
          <ul class="dropdown-menu">
            <li><a class="dropdown-item" href="#">Felhasználó kezelése</a></li>
            <li><a class="dropdown-item" href="#">Kurzusok kezelése</a></li>
            <li><a class="dropdown-item" href="#">Csoportok kezelése</a></li>
          </ul>
        </li>
      </ul>
    </div>
  </div>
</nav>
</template>

<script setup></script>
