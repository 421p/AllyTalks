**Introducing controllers**

```php
// WebApp/Controller/BasicController.php

class BasicController extends Controller
{
    /**
     * @controller
     * @method GET
     * @route /helloworld
     */
    public function helloWorld()
    {
        return 'hello world';
    }

```

PHPDoc annotations is used for describing controllers.

`@controller` - any method marked as controller will be attached to the application. <br>
`@method` - get, post, etc.<br>
`@route` - route to this controller, for example `mysite.loc/helloworld`

*This is a wrapper for Silex routes so all stuff like dynamic routing and Symfony request/response can be used here.*

```php
/**
 * @controller
 * @method GET
 * @route /hello/{name}
 */
public function helloSomeone($name)
{
    return 'hello '.$name;
}
```

```php
/**
 * @controller
 * @method GET
 * @route /hello
 */
public function helloAgain(Request $request, Response $response)
{
    $someParam = $request->query->get('...');
    
    $response->setHeader('...');
    
    return $response;
}
```

You can register controller by adding it to `Application::registerControllers` method:

```php
// WebApp/Application.php

private function registerControllers()
{
    $this->controllers = [
        new BasicController()
        new MegaOmegaController($params),
        ...
    ];
}
```
