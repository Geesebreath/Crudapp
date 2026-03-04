# Crudman
Badly named Docker image for getting the status of a bunch of URL's.

This was made to help out a friend with a quick and readable way to check if their services are still up and running. It was also a good way to get stuck into learning more about web dev with Blazor!

The app consists of a database of URLs and a home page with a Check button to get the current HTML status of all the URLs in the database. It has a built in 5 second timeout, follows redirects, and URLs can be custom ordered.
<img width="998" height="571" alt="image" src="https://github.com/user-attachments/assets/2254a67b-e7f0-4be8-8612-d8f7be7d03fb" />
<img width="1001" height="486" alt="image" src="https://github.com/user-attachments/assets/caf362d7-e276-4113-8a7f-1ba1cca4fcbc" />


Nothing fancy, but it was fun to work on!

Before running the Docker image, a folder needs to be created to store the URL database locally. When running, a port needs to bemapped to the container's port 8080 and also bind-mount your created database folder to the "Crudmandb" folder in the container.

Example command to pull the image: sudo docker run -p 80:8080 -v ../DatabaseFolder:/Crudmandb docker pull ghcr.io/geesebreath/crudman:main
