# Crudman
Badly named Docker image for getting the status of a bunch of URL's.

This was made to help out a friend with a quick and readable way to check if their services are still up and running. It was also a good way to get stuck into learning more about web dev with Blazor!

The app consists of a database of URLs and a home page with a Check button to get the current HTML status of all the URLs in the database. It has a built in 5 second timeout, follows redirects, and URLs can be custom ordered.
<img width="998" height="571" alt="image" src="https://github.com/user-attachments/assets/2254a67b-e7f0-4be8-8612-d8f7be7d03fb" />
<img width="1001" height="486" alt="image" src="https://github.com/user-attachments/assets/caf362d7-e276-4113-8a7f-1ba1cca4fcbc" />


Nothing fancy, but it was fun to work on!

Run this command to get and run the image with Docker:
```
docker run -p 8080:8080 -v /d:/Crudmandb ghcr.io/geesebreath/crudman:main
```
"/d" is a local directory to bind-mount a volume for the database of URLs to persist. You can use a docker volume if preferred.
