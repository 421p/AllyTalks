<?php

namespace AllyTalks\WebApp;

use Silex\Application as SilexApp;
use Silex\Provider\TwigServiceProvider;

class Application extends SilexApp
{
    /** @var  \Twig_Environment */
    private $twig;

    private $model;

    public function __construct()
    {
        parent::__construct();

        $this->model = new Model();

        $this->registerComponents();
        $this->registerRoutes();
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

    private function registerRoutes()
    {
        $this->get('/', [$this, 'mainPageController']);
        $this->get('/users', [$this, 'userListController']);
    }

    public function mainPageController()
    {
        return $this->twig->render('index.twig');
    }

    public function userListController()
    {
        return $this->twig->render('users.twig', [
            'users' => $this->model->getAllUsers(),
        ]);
    }
}
