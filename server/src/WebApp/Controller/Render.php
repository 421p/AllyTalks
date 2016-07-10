<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\WebApp\Application;

class Render extends Controller
{
    private $app;
    private $twig;

    public function __construct(Application $application)
    {
        $this->twig = $application->getTwig();
        $this->app = $application;
    }

    /**
     * @controller
     *
     * @method GET
     * @route /
     */
    public function mainPageController()
    {
        return $this->twig->render('index.twig');
    }

    /**
     * @controller
     *
     * @method GET
     * @route /register
     */
    public function registrationPageController()
    {
        return $this->twig->render('register.twig');
    }

   
}
