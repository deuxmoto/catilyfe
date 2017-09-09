cd ..
git pull
printf "\nRunning 'ng build'...\n"
ng build
printf "\nCopying dist to server folder...\n"
mkdir -p ./server/public
cp -r ./dist/* ./server/public
printf "Static files updated!\n"
