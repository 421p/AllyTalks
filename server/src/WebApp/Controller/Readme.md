**Introducing controllers**

```php
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

PHDoc annotation is user for describing the controllers.

`@controller` - any method marked as controller will be attached to the application.
`@method` - get, post, etc.
`@route` - route to this controller, for example `mysite.loc/helloworld`

*This is a wrapper for Silex routes, so anything like dynamic routing and Symfony's request/response can be used here.*

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