## Dis be
# CATILYFE FRONTEND SERVER

Some notes about this:

- It's just a static content server.
- Since it's just a static content server, it doesn't have any of the crappy overhead of serving the frontend from an IIS server.
- The static files are served from the "public" folder, but they're not actually checked in. Instead, they're copied from "../dist" when `ng build` is run.
- It's dank.
