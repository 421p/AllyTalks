We're using [Silex](http://silex.sensiolabs.org/) for web application and [Doctrine](http://doctrine-orm.readthedocs.io/projects/doctrine-orm/en/latest/tutorials/getting-started.html)
as database abstraction tool.

Here is few scripts that will help with maintenance:

* `bin/doctrine` is shortcut for a `vendor/bin/doctrine` that can be found in docs.
* `bin/fixtures` will drop && recreate database and load fixtures from `ORM/Fixtures/user.yml`. [alice](https://github.com/nelmio/alice) is used for it.
