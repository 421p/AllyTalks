<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\Utils\Exception\SpookyException;
use AllyTalks\WebApp\Application;
use Symfony\Component\HttpFoundation\Request;

class Test extends Controller
{
    private $app;

    public function __construct(Application $application)
    {
        $this->app = $application;
    }

    /**
     * @controller
     * @method GET
     * @route /users
     */
    public function userList()
    {
        return $this->app->getTwig()->render(
            'users.twig',
            [
                'users' => $this->app->getModel()->getAllUsers(),
            ]
        );
    }

    /**
     * @controller
     * @method GET
     * @route /echo
     * @param Request $request
     */
    public function echo (Request $request)
    {
        return sprintf(
            'request url: %s, <br>
            echo: %s',
            $request->getQueryString(),
            $request->query->get('echo')
        );
    }

    /**
     * @controller
     * @method GET
     * @route /exception
     */
    public function exception()
    {
        throw new SpookyException('Spooky exception was thrown!!!');
    }
}