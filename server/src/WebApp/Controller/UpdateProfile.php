<?php

namespace AllyTalks\WebApp\Controller;

use AllyTalks\Assertion\Assertion;
use AllyTalks\Utils\Exception\JsonException;
use AllyTalks\Utils\Exception\SpookyException;
use AllyTalks\WebApp\Application;
use Symfony\Component\HttpFoundation\JsonResponse;
use Symfony\Component\HttpFoundation\Response;
use Symfony\Component\HttpFoundation\Request;

class UpdateProfile extends Controller
{
    private $app;
    private $model;

    public function __construct(Application $application)
    {
        $this->app = $application;
        $this->model = $this->app->getModel();
    }

    /**
     * @controller
     *
     * @method POST
     * @route /api/update
     *
     * @param Request $request
     *
     * @return Response
     */
    public function updateUserInfo(Request $request)
    {
        return new Response('Thank you for your registration!');
    }
}
