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
     * @param array $errors
     * @param array $data
     */
    public function registrationPageController($errors=[], $data=[])
    {
        return $this->twig->render('register.twig', array_merge($errors, $data));
    }
}
