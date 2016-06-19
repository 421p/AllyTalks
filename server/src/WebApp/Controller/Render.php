<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\WebApp\Application;

class Render extends Controller
{
    private $twig;

    public function __construct(Application $application)
    {
        $this->twig = $application->getTwig();
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
}
