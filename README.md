# AllyTalks

### something like a chat application

How to run a prototype:

1. `vagrant up`
2.  connect to box via `vagrant ssh` or `PuTTy`
3. `cd /vagrant`
4. `composer install`
5. start prototype server via `php server/bin/prototype`
6. build and start c# application

For building a normal server:

1. `vagrant up`
2.  connect to box via `vagrant ssh` or `PuTTy`
3. `cd /vagrant`
4. `composer install`
5. `php server/bin/doctrine orm:schema-tool:create`
6. `php server/bin/fixtures`
7. start it via `php server/bin/wcs`

Both servers will use `7777` port so don't try to run two at once.

For a website using you need to add `192.168.56.101 allytalks.loc` to your hosts file 
(on windows `C:\Windows\System32\drivers\etc`) and also `192.168.56.101 allytalks.spa` for single-page app.

Shortly about folders:
`frontend` - c# app
`frontend-js` - js spa available at `allytalks.spa` , webroot at `frontend-js/www`
`server` - full php stack
`server/www` - webroot for `allytalks.loc`