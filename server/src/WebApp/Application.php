<?php

namespace AllyTalks\WebApp;

use AllyTalks\Utils\Exception\JsonException;
use AllyTalks\Utils\Exception\SpookyException;
use AllyTalks\WebApp\Controller\Auth;
use AllyTalks\WebApp\Controller\ControllerInterface;
use AllyTalks\WebApp\Controller\Registration;
use AllyTalks\WebApp\Controller\Render;
use AllyTalks\WebApp\Controller\Test;
use AllyTalks\WebApp\Controller\UpdateProfile;
use Silex\Application as SilexApp;
use Silex\Provider\TwigServiceProvider;
use Symfony\Component\HttpFoundation\Response;

class Application extends SilexApp
{
    /** @var  \Twig_Environment */
    private $twig;

    private $model;

    /** @var ControllerInterface[] */
    private $controllers = [];

    public function __construct()
    {
        parent::__construct();

        $this->model = new Model();

        $this->registerComponents();
        $this->registerMiddleware();

        $this->registerControllers();
        $this->attachControllers();
    }

    private function registerComponents()
    {
        $this->register(
            new TwigServiceProvider(),
            [
                'twig.path' => __DIR__.'/views',
            ]
        );

        $this->twig = $this['twig'];
    }

    private function registerMiddleware()
    {
        $this->error(
            function (\Exception $e, $code) {
                if ($e instanceof SpookyException) {
                    return new Response($e->getMessage(), 401);
                } elseif ($e instanceof JsonException) {
                    return new Response($e->getMessage(), $e->getStatusCode());
                }

                throw $e;
            }
        );
    }

    private function registerControllers()
    {
        $this->controllers = [
            new Test($this),
            new Render($this),
            new Registration($this),
            new UpdateProfile($this),
            new Auth($this->model),
        ];
    }

    private function attachControllers()
    {
        foreach ($this->controllers as $i => $controller) {
            foreach ($controller->getRoutes() as $methodName => $route) {
                $method = $route['method'];
                $route = $route['route'];

                $this->$method($route, [$this->controllers[$i], $methodName]);
            }
        }
    }

    public function getTwig() : \Twig_Environment
    {
        return $this->twig;
    }

    public function getModel() : Model
    {
        return $this->model;
    }
}
