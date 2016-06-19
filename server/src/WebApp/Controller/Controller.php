<?php

namespace AllyTalks\WebApp\Controller;

abstract class Controller implements ControllerInterface
{
    public function getRoutes() : array
    {
        $routesData = [];

        $reflector = new \ReflectionClass(static::class);

        foreach ($reflector->getMethods() as $route) {
            $doc = $route->getDocComment();

            if (preg_match('#@controller#', $doc)) {
                $routesData[$route->getName()] = $this->getAnnotationData($doc);
            }
        }

        return $routesData;
    }

    private function getAnnotationData(string $doc) : array
    {
        preg_match('#@method\s(?<method>.+)#', $doc, $matches);

        $method = $matches['method'];

        preg_match('#@route\s(?<route>.+)#', $doc, $matches);

        $route = $matches['route'];

        return [
            'method' => strtolower($method),
            'route' => $route,
        ];
    }
}
