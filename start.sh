if [ -f ".env" ]; then
    echo ".env fájl már létezik!"
else 
    cp .env.example .env
fi

if [ -f "frontend/node_modules" ]; then
    echo "frontend/node_modules már létezik!"
else 
    docker run -v "$(pwd)/frontend:/app" -it --entrypoint npm idomi27/vue install
fi

docker compose up -d --build

if [ -f "backend/vendor" ]; then
    echo "backend/vendor már létezik!"
else 
    docker compose exec app composer install
fi

source .env

if [ -z "${APP_KEY}" ]; then
    echo "APP_KEY már létezik!"
else
    docker compose exec app php artisan key:generate
fi

docker compose exec app php artisan migrate:fresh --seed