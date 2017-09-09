cd ..
echo "Running 'ng build'..."
ng build
echo "Copying dist to server folder..."
mkdir -p ./server/public
cp -r ./dist/* ./server/public
echo "Static files updated!"
