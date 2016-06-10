<?php

namespace AllyTalks\WebApp;

use Silex\Application as SilexApp;
use Silex\Provider\TwigServiceProvider;

class Application extends SilexApp
{
    /** @var  \Twig_Environment */
    public $twig;

    public function __construct()
    {
        parent::__construct();
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

    private function registerRoutes() {
        $this->get('/', [$this, 'mainPageController']);
    }

    public function mainPageController()
    {
        return $this->twig->render('index.twig');
    }
}